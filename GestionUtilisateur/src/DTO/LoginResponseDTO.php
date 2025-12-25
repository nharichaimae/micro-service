<?php

namespace App\DTO;

class LoginResponseDTO
{
    public bool $authenticated;
    public string $token;
    public int $id;
    public string $email;

    public function __construct(bool $authenticated, string $token, int $id, string $email)
    {
        $this->authenticated = $authenticated;
        $this->token = $token;
        $this->id = $id;
        $this->email = $email;
    }
}
