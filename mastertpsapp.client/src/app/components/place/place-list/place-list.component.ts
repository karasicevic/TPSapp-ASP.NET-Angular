import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'; // Importuj Router
import { PlaceService, Place } from 'src/app/services/place.service';

@Component({
  selector: 'app-place-list',
  templateUrl: './place-list.component.html',
  styleUrls: ['./place-list.component.css']
})
export class PlaceListComponent implements OnInit {
  places: Place[] = [];

  constructor(private placeService: PlaceService, private router: Router) { } // Dodaj Router u constructor

  ngOnInit(): void {
    this.placeService.getPlaces().subscribe(
      (data: Place[]) => this.places = data,
      error => console.error('Error fetching places', error)
    );
  }

  viewPlace(id: number): void {
    this.router.navigate(['/places', id]);
  }

  editPlace(id: number): void {
    this.router.navigate(['/place-form', id]);
  }

  deletePlace(id: number): void {
    // Implementiraj funkcionalnost brisanja (u servisu treba dodati metodu za brisanje)
    this.placeService.deletePlace(id).subscribe(
      () => this.places = this.places.filter(place => place.id !== id),
      error => console.error('Error deleting place', error)
    );
  }
}
