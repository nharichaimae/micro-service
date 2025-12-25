<?php

declare(strict_types=1);

namespace DoctrineMigrations;

use Doctrine\DBAL\Schema\Schema;
use Doctrine\Migrations\AbstractMigration;

/**
 * Auto-generated Migration: Please modify to your needs!
 */
final class Version20251217141648 extends AbstractMigration
{
    public function getDescription(): string
    {
        return '';
    }

    public function up(Schema $schema): void
    {
        // this up() migration is auto-generated, please modify it to your needs
        $this->addSql('CREATE TABLE messenger_messages (id BIGINT AUTO_INCREMENT NOT NULL, body LONGTEXT NOT NULL, headers LONGTEXT NOT NULL, queue_name VARCHAR(190) NOT NULL, created_at DATETIME NOT NULL, available_at DATETIME NOT NULL, delivered_at DATETIME DEFAULT NULL, INDEX IDX_75EA56E0FB7336F0 (queue_name), INDEX IDX_75EA56E0E3BD61CE (available_at), INDEX IDX_75EA56E016BA31DB (delivered_at), PRIMARY KEY (id)) DEFAULT CHARACTER SET utf8mb4 COLLATE `utf8mb4_unicode_ci`');
        $this->addSql('ALTER TABLE equipement DROP FOREIGN KEY `equipement_ibfk_1`');
        $this->addSql('ALTER TABLE piece DROP FOREIGN KEY `piece_ibfk_1`');
        $this->addSql('DROP TABLE equipement');
        $this->addSql('DROP TABLE piece');
        $this->addSql('ALTER TABLE auth_token CHANGE token token VARCHAR(255) DEFAULT NULL, CHANGE expirest_at expirest_at DATETIME DEFAULT NULL');
        $this->addSql('ALTER TABLE user CHANGE email email VARCHAR(255) DEFAULT NULL, CHANGE password password VARCHAR(255) DEFAULT NULL');
    }

    public function down(Schema $schema): void
    {
        // this down() migration is auto-generated, please modify it to your needs
        $this->addSql('CREATE TABLE equipement (id_equipement INT AUTO_INCREMENT NOT NULL, nom VARCHAR(100) CHARACTER SET utf8mb4 DEFAULT NULL COLLATE `utf8mb4_0900_ai_ci`, description VARCHAR(255) CHARACTER SET utf8mb4 DEFAULT NULL COLLATE `utf8mb4_0900_ai_ci`, etat VARCHAR(10) CHARACTER SET utf8mb4 DEFAULT \'OFF\' COLLATE `utf8mb4_0900_ai_ci`, id INT DEFAULT NULL, INDEX id (id), PRIMARY KEY (id_equipement)) DEFAULT CHARACTER SET utf8mb4 COLLATE `utf8mb4_0900_ai_ci` ENGINE = InnoDB COMMENT = \'\' ');
        $this->addSql('CREATE TABLE piece (id INT AUTO_INCREMENT NOT NULL, name VARCHAR(255) CHARACTER SET utf8mb4 NOT NULL COLLATE `utf8mb4_0900_ai_ci`, user_id INT NOT NULL, created_at DATETIME DEFAULT CURRENT_TIMESTAMP, INDEX user_id (user_id), PRIMARY KEY (id)) DEFAULT CHARACTER SET utf8mb4 COLLATE `utf8mb4_0900_ai_ci` ENGINE = InnoDB COMMENT = \'\' ');
        $this->addSql('ALTER TABLE equipement ADD CONSTRAINT `equipement_ibfk_1` FOREIGN KEY (id) REFERENCES piece (id) ON UPDATE NO ACTION ON DELETE CASCADE');
        $this->addSql('ALTER TABLE piece ADD CONSTRAINT `piece_ibfk_1` FOREIGN KEY (user_id) REFERENCES user (id) ON UPDATE NO ACTION ON DELETE CASCADE');
        $this->addSql('DROP TABLE messenger_messages');
        $this->addSql('ALTER TABLE auth_token CHANGE token token VARCHAR(255) NOT NULL, CHANGE expirest_at expirest_at DATETIME NOT NULL');
        $this->addSql('ALTER TABLE `user` CHANGE email email VARCHAR(255) NOT NULL, CHANGE password password VARCHAR(255) NOT NULL');
    }
}
