import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PlaceService, Place } from 'src/app/services/place.service';

@Component({
  selector: 'app-place-detail',
  templateUrl: './place-detail.component.html',
  styleUrls: ['./place-detail.component.css']
})
export class PlaceDetailComponent implements OnInit {
  place: Place | undefined;
  stats: any;

  constructor(
    private route: ActivatedRoute,
    private placeService: PlaceService
  ) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.placeService.getPlace(id).subscribe(data => {
      this.place = data,
          ( error: any) => console.error('Error fetching place details', error)
    });
    this.placeService.getPlaceStats(id).subscribe(stats => {
      this.stats = stats;
      console.log(stats)
    });
  }
}
