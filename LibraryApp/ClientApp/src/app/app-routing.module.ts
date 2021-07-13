import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AudiobookListComponent } from './audiobook-list/audiobook-list.component';
import { BookListComponent } from './book-list/book-list.component';
import { EbookListComponent } from './ebook-list/ebook-list.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { ProfileComponent } from './profile/profile.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  { path: "", component: HomeComponent },
  { path: "home", component: HomeComponent },
  { path: "login", component: LoginComponent },
  { path: "register", component: RegisterComponent },
  { path: "books", component: BookListComponent },
  { path: "ebooks", component: EbookListComponent },
  { path: "audiobooks", component: AudiobookListComponent },
  { path: "profile", component: ProfileComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
