import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PlaceService } from '../../../services/place.service';
import { Place } from '../../../services/place.service';

@Component({
  selector: 'app-place-form',
  templateUrl: './place-form.component.html',
  styleUrls: ['./place-form.component.css']
})
export class PlaceFormComponent implements OnInit {
  placeForm: FormGroup;
  placeId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private placeService: PlaceService
  ) {
    this.placeForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(40), Validators.pattern('^[A-ZČĆŽŠĐ][a-zčćžšđ]+(\s[A-Za-zčćžšđČĆŽŠĐ]+)*$')
]],
      postalCode: ['', [Validators.required, Validators.min(11000), Validators.max(45000)]],
      population: ['', [Validators.required, Validators.min(0), Validators.max(2000000)]]
    });
  }

  ngOnInit(): void {
    this.placeId = Number(this.route.snapshot.paramMap.get('id'));
    if (this.placeId) {
      this.placeService.getPlace(this.placeId).subscribe(
        (place: Place) => this.placeForm.patchValue(place),
        error => console.error('Error fetching place details', error)
      );
    }
  }

  onSubmit(): void {
    if (this.placeForm.valid) {
      const place: Place = this.placeForm.value;
      console.log(place)
      if (this.placeId) {
        this.placeService.updatePlace(this.placeId, place).subscribe(
          () => this.router.navigate(['/places']),
          error => console.error('Error updating place', error)
        );
      } else {
        this.placeService.addPlace(place).subscribe(
          (response: Place) => {
            console.log('Place added successfully', response);
            this.router.navigate(['/places']);
          },
          error => {
            console.error('Error adding place', error);
          }
        );
      }
    }
  }
}
