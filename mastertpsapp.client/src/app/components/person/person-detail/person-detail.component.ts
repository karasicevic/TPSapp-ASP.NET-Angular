import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PersonService, Person } from 'src/app/services/person.service';
import { PlaceService } from '../../../services/place.service';

@Component({
  selector: 'app-person-detail',
  templateUrl: './person-detail.component.html',
  styleUrls: ['./person-detail.component.css']
})
export class PersonDetailComponent implements OnInit {
  person?: Person;
  places: any[] = [];

  constructor(
    private route: ActivatedRoute,
    private personService: PersonService,
    private placeService: PlaceService
  ) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.personService.getPerson(id).subscribe(
      (data: Person) => this.person = data,
      error => console.error('Error fetching person details', error)
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
}

