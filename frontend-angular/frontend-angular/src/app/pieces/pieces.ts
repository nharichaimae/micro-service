import { Component } from '@angular/core';
import { Piece } from '../services/piece'; 
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pieces',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './pieces.html',
  styleUrls: ['./pieces.css'],
})
export class Pieces {
  name = '';

  constructor(
    private piece: Piece,
    private router: Router   
  ) {}

  add() {
    if (!this.name) return;

    this.piece.addPiece(this.name).subscribe({
      next: (res: any) => {
        console.log('Pièce ajoutée !');
        this.name = '';
        const id = res.id;
        this.router.navigate(['/add-equipement', id])
      },
      error: (err) => console.error(err)
    });
  }
  goTo(path: string) {
    this.router.navigate([path]);
  }
}
