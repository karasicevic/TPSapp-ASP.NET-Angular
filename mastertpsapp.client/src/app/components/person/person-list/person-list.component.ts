import { Component, OnInit } from '@angular/core';
import { PersonService, Person } from 'src/app/services/person.service';
import { Router } from '@angular/router';
import { PlaceService } from '../../../services/place.service';

@Component({
  selector: 'app-person-list',
  templateUrl: './person-list.component.html',
  styleUrls: ['./person-list.component.css']
})
export class PersonListComponent implements OnInit {
  persons: Person[] = [];
  places: any[] = []; 

  constructor(private personService: PersonService, private router: Router,
    private placeService: PlaceService) { }

  ngOnInit(): void {
    this.personService.getPersons().subscribe(
      (data: Person[]) => this.persons = data,
      error => console.error('Error fetching persons', error)
    );

    this.placeService.getPlaces().subscribe((data: any[]) => {
      this.places = data;
    });
  }

  getPlaceName(placeId: number | undefined): string {
    if (placeId === undefined) {
      return 'N/A';
    }
    const place = this.places.find(p => p.id === placeId);
    return place ? place.name : 'Unknown';
  }

  viewPerson(id: number): void {
    this.router.navigate(['/persons', id]);
  }

  editPerson(id: number): void {
    this.router.navigate(['/person-form', id]);
  }

  deletePerson(id: number): void {
    if (confirm('Are you sure you want to delete this person?')) {
      this.personService.deletePerson(id).subscribe(() => {
        this.persons = this.persons.filter(person => person.id !== id);
      });
    }
  }
}
