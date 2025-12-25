import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Piece } from '../services/piece';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.html',
  styleUrls: ['./dashboard.css'],
})
export class Dashboard implements OnInit {
  pieces: any[] = [];

  constructor(private router: Router, private piece: Piece, private cd: ChangeDetectorRef) {}

  ngOnInit(): void {
    const userId = Number(localStorage.getItem('user_id'));
    if (!userId) {
      console.error('userId invalide');
      return;
    }

    this.loadPieces(userId);
  }



loadPieces(userId: number) {
  this.piece.getPiecesWithEquipements(userId).subscribe({
    next: (data) => {
      this.pieces = data; 
      this.cd.detectChanges(); 
    },
    error: (err) => console.error('Erreur chargement pièces:', err)
  });
}

  goTo(path: string) {
    this.router.navigate([path]);
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user_id');
    this.router.navigate(['/login']);
  }
}
