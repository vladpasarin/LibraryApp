import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { HomeComponent } from './home/home.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenAuthInterceptor } from './shared/interceptors/token-auth.interceptor';
import {ToastrModule} from 'ngx-toastr';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatCarouselModule } from '@ngbmodule/material-carousel';
import { MatCardModule } from '@angular/material/card';
import { BookCardComponent } from './book-card/book-card.component';
import { MatButtonModule } from '@angular/material/button';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { IvyCarouselModule } from 'angular-responsive-carousel';
import { BookListComponent } from './book-list/book-list.component';
import { EbookListComponent } from './ebook-list/ebook-list.component';
import { AudiobookListComponent } from './audiobook-list/audiobook-list.component';
import { ProfileComponent } from './profile/profile.component';
import { FormsModule } from '@angular/forms';
import { SearchFilterPipe } from './searchfilter.pipe';
import { AssetProfileComponent } from './asset-profile/asset-profile.component';
import { MatLabel } from '@angular/material/form-field';
import { MatTabNav, MatTabsModule } from '@angular/material/tabs';
import { MatIconModule } from '@angular/material/icon';
import {MatChipsModule} from '@angular/material/chips';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    BookCardComponent,
    BookListComponent,
    EbookListComponent,
    AudiobookListComponent,
    ProfileComponent,
    SearchFilterPipe,
    AssetProfileComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FontAwesomeModule,
    ReactiveFormsModule,
    HttpClientModule,
    ToastrModule.forRoot(),
    MatDatepickerModule,
    MatNativeDateModule,
    MatFormFieldModule,
    MatInputModule,
    MatSnackBarModule,
    FontAwesomeModule,
    BrowserAnimationsModule,
    MatCarouselModule.forRoot(),
    MatCardModule,
    MatButtonModule,
    MDBBootstrapModule.forRoot(),
    IvyCarouselModule,
    FormsModule,
    MatTabsModule,
    MatChipsModule,
    MatIconModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: TokenAuthInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
