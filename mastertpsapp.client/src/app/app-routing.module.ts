import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PlaceListComponent } from './components/place/place-list/place-list.component';
import { PlaceFormComponent } from './components/place/place-form/place-form.component';
import { PlaceDetailComponent } from './components/place/place-detail/place-detail.component';
import { PersonListComponent } from './components/person/person-list/person-list.component';
import { PersonDetailComponent } from './components/person/person-detail/person-detail.component';
import { PersonFormComponent } from './components/person/person-form/person-form.component';

const routes: Routes = [
  { path: 'places', component: PlaceListComponent },
  { path: 'places/new', component: PlaceFormComponent },
  { path: 'places/:id', component: PlaceDetailComponent },
  { path: 'place-form', component: PlaceFormComponent },
  { path: 'place-form/:id', component: PlaceFormComponent },
  { path: 'persons', component: PersonListComponent },
  { path: 'persons/:id', component: PersonDetailComponent },
  { path: 'person-form', component: PersonFormComponent },
  { path: 'persons/new', component: PersonFormComponent },
  { path: 'person-form/:id', component: PersonFormComponent },
  { path: '', redirectTo: '/persons', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
