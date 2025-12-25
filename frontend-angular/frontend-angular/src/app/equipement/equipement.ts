import { Component, OnInit } from '@angular/core';
import { ActivatedRoute ,Router} from '@angular/router';
import { EquipementService  } from '../services/Equipement';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-equipement',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './equipement.html',
  styleUrls: ['./equipement.css']
})
export class equipement implements OnInit {

  equipement = {
    nom: '',
    description: '',
    id: 0
  };

  constructor(
    private route: ActivatedRoute,
    private equipementService: EquipementService,
    private router: Router  
  ) {}

  ngOnInit() {
    this.equipement.id= Number(
      this.route.snapshot.paramMap.get('id')
    );
  }

  ajouter() {
    this.equipementService.addEquipement(this.equipement)
      .subscribe({
        next: () => {
          alert('Équipement ajouté avec succès');
           this.router.navigate(['/dashboard']);
        },
        error: () => {
          alert('Erreur lors de l’ajout');
        }
      });
  }
    goTo(path: string) {
    this.router.navigate([path]);
  }
}
