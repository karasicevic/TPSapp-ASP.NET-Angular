import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { PlaceListComponent } from './components/place/place-list/place-list.component';
import { PlaceDetailComponent } from './components/place/place-detail/place-detail.component';
import { PlaceFormComponent } from './components/place/place-form/place-form.component';
import { AppRoutingModule } from './app-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; 
import { MatFormFieldModule } from '@angular/material/form-field'; 
import { MatInputModule } from '@angular/material/input'; 
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatListModule } from '@angular/material/list';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterModule } from '@angular/router';
import { FooterComponent } from './components/global-components/footer/footer.component';
import { HeaderComponent } from './components/global-components/header/header.component';
import { PersonListComponent } from './components/person/person-list/person-list.component';
import { PersonFormComponent } from './components/person/person-form/person-form.component';
import { PersonDetailComponent } from './components/person/person-detail/person-detail.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule, MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';

@NgModule({
  declarations: [
    AppComponent,
    PlaceListComponent,
    PlaceDetailComponent,
    PlaceFormComponent,
    FooterComponent,
    HeaderComponent,
    PersonListComponent,
    PersonFormComponent,
    PersonDetailComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule,
    RouterModule,
    MatSelectModule,
    BrowserAnimationsModule, 
    MatFormFieldModule, 
    MatInputModule, 
    MatButtonModule, 
    MatIconModule,
    MatCardModule, 
    MatListModule,
    MatToolbarModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatOptionModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
