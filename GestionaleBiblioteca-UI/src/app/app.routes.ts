import { Routes } from '@angular/router';
import { BookListComponent } from './components/book-list/book-list';
import { AuthorListComponent } from './components/author-list/author-list';

export const routes: Routes = [
  { path: 'libri', component: BookListComponent },
  { path: 'autori', component: AuthorListComponent },
  { path: '', redirectTo: '/libri', pathMatch: 'full' } // Default
];
