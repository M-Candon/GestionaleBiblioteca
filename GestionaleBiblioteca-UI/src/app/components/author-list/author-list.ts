import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BookService } from '../../services/book'; // Usiamo lo stesso service
import { Author } from '../../models/library.model';

@Component({
  selector: 'app-author-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './author-list.html'
})
export class AuthorListComponent implements OnInit {
  authors: Author[] = [];
  showForm = false;
  newAuthor: Author = { id: 0, name: '', surname: '' };
  authorToEdit?: Author;
  constructor(private bookService: BookService, private cd: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.loadAuthors();
  }

  loadAuthors() {
    this.bookService.getAuthors().subscribe(data => {
      this.authors = data;
      this.cd.detectChanges();
    });
  }

 saveAuthor() {
  if (this.newAuthor.id > 0) {
    this.bookService.updateAuthor(this.newAuthor.id, this.newAuthor).subscribe({
      next: () => {
        this.loadAuthors(); // Ricarica la lista
        this.resetForm();   // Svuota e chiudi
      }
    });
  } else {
    this.bookService.addAuthor(this.newAuthor).subscribe({
      next: () => {
        this.loadAuthors();
        this.resetForm();
      }
    });
  }
}

// Modifica anche il toggle per pulire i dati se si chiude manualmente
toggleForm() {
  if (this.showForm) {
    this.resetForm(); // Se è aperto, lo chiudo e resetto
  } else {
    this.showForm = true; // Se è chiuso, lo apro (newAuthor è già 0 di default)
  }
}

resetForm() {
  // 1. Riporta l'oggetto allo stato "Nuovo" (ID = 0 è la chiave per il Backend)
  this.newAuthor = { 
    id: 0, 
    name: '', 
    surname: '' 
  };

  // 2. Nasconde il form
  this.showForm = false;

  // 3. Pulisce l'eventuale riferimento all'autore che stavamo modificando
  this.authorToEdit = undefined;

  // 4. Avvisa Angular di aggiornare la grafica
  this.cd.detectChanges();
}

  deleteAuthor(id: number) {
    if (confirm('Eliminare l\'autore? Verranno rimossi anche i suoi libri.')) {
      this.bookService.deleteAuthor(id).subscribe(() => {
        this.authors = this.authors.filter(a => a.id !== id);
        this.cd.detectChanges();
      });
    }
  }

  editAuthor(author: Author) {
  this.authorToEdit = { ...author };
  this.newAuthor = { ...author }; // Popoliamo i campi del form
  this.showForm = true;
}
}