<?php

namespace App\Controller;

use App\Entity\AuthToken;
use App\DTO\LoginRequestDTO;
use App\DTO\LoginResponseDTO;
use App\DTO\UserResponseDTO;
use App\Repository\UserRepository;
use Doctrine\ORM\EntityManagerInterface;
use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\JsonResponse;
use Symfony\Component\Routing\Annotation\Route;

class AuthController extends AbstractController
{
    #[Route('/api/login', name: 'api_login', methods: ['POST'])]
    public function login(
        Request $request,
        UserRepository $userRepository,
        EntityManagerInterface $em
    ): JsonResponse {
        $data = json_decode($request->getContent(), true);

        if (!$data || !isset($data['email'], $data['password'])) {
            return new JsonResponse(
                ['message' => 'Email et mot de passe requis'],
                400
            );
        }

        $loginDTO = new LoginRequestDTO();
        $loginDTO->email = $data['email'];
        $loginDTO->password = $data['password'];

        $user = $userRepository->findOneBy(['email' => $loginDTO->email]);

      
        if (!$user || $user->getPassword() !== $loginDTO->password) {
            return new JsonResponse(
                ['authenticated' => false, 'message' => 'Identifiants invalides'],
                401
            );
        }

        $tokenString = bin2hex(random_bytes(32));

        $authToken = new AuthToken();
        $authToken->setUser($user);
        $authToken->setToken($tokenString);
        $authToken->setExpirestAt(new \DateTime('+1 day'));

        $em->persist($authToken);
        $em->flush();

        $responseDTO = new LoginResponseDTO(
            true,
            $tokenString,
            $user->getId(),
            $user->getEmail()
        );

        return new JsonResponse($responseDTO, 201); 
    }

    #[Route('/api/user-by-token', name: 'api_user_by_token', methods: ['GET'])]
    public function getUserByToken(
        Request $request,
        EntityManagerInterface $em
    ): JsonResponse {
        $token = $request->query->get('token');

        if (!$token) {
            return new JsonResponse(['message' => 'Token manquant'], 400);
        }

        $authToken = $em->getRepository(AuthToken::class)
            ->findOneBy(['token' => $token]);

        if (!$authToken || $authToken->getExpirestAt() < new \DateTime()) {
            return new JsonResponse(['message' => 'Token invalide ou expiré'], 401);
        }

        $user = $authToken->getUser();
        $userDTO = new UserResponseDTO(
            $user->getId(),
            $user->getEmail()
        );

        return new JsonResponse($userDTO, 200); 
    }
}
