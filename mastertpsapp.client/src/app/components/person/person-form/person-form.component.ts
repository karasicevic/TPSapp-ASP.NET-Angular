import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PersonService, Person } from 'src/app/services/person.service';
import { PlaceService } from '../../../services/place.service';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';

@Component({
  selector: 'app-person-form',
  templateUrl: './person-form.component.html',
  styleUrls: ['./person-form.component.css']
})
export class PersonFormComponent implements OnInit {
  personForm: FormGroup;
  isEdit = false;
  places: any[] = [];
  today: Date = new Date();

  constructor(
    private fb: FormBuilder,
    private personService: PersonService,
    private route: ActivatedRoute,
    private router: Router,
    private placeService: PlaceService
  ) {
    this.personForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(33), Validators.pattern('^[A-ZČĆŽŠĐ][a-zA-ZčćžšđČĆŽŠĐ]*$')]],
      surname: ['', [Validators.required, Validators.maxLength(33), Validators.pattern('^[A-ZČĆŽŠĐ][a-zA-ZčćžšđČĆŽŠĐ]*$')]],
      birthDate: ['', [Validators.required, this.validateFutureDate]],
      personalIdNumber: ['', [Validators.required, Validators.pattern('^\\d{13}$')]],
      height: ['', [Validators.required]],
      placeOfBirthId: ['', [Validators.required]],
      placeOfResidenceId: ['', [Validators.required]]
    });

    this.loadPlaces();
  }

  loadPlaces(): void {
    this.placeService.getPlaces().subscribe((data: any[]) => {
      this.places = data;
    });
  }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (id) {
      this.isEdit = true;
      this.personService.getPerson(id).subscribe(
        person => this.personForm.patchValue(person),
        error => console.error('Error fetching person for edit', error)
      );
    }
  }

  validateFutureDate(control: any): { [key: string]: boolean } | null {
    const today = new Date();
    const birthDate = new Date(control.value);
    if (birthDate > today) {
      return { futureDate: true };
    }
    return null;
  }

  onSubmit(): void {
    if (this.personForm.valid) {
      if (this.isEdit) {
        const id = Number(this.route.snapshot.paramMap.get('id'));
        this.personService.updatePerson(id, this.personForm.value).subscribe(
          () => this.router.navigate(['/persons']),
          error => console.error('Error updating person', error)
        );
      } else {
        this.personService.createPerson(this.personForm.value).subscribe(
          () => this.router.navigate(['/persons']),
          error => console.error('Error creating person', error)
        );
      }
    }
  }
}
