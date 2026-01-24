import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Book } from '../../models/library.model';
import { BookService } from '../../services/book';

// Forza VS Code a ricaricare il percorso corretto
import { BookFormComponent } from '../book-form/book-form';
@Component({
  selector: 'app-book-list',
  standalone: true,
  imports: [CommonModule, BookFormComponent, FormsModule],
  templateUrl: './book-list.html'
})
export class BookListComponent implements OnInit {
  books: Book[] = [];
  showForm: boolean = false; // <-- Variabile per mostrare/nascondere il form
  searchTerm: string = '';
  constructor(private bookService: BookService,
    private cd: ChangeDetectorRef
  ) {}

toggleForm() {
  this.showForm = !this.showForm;
  
  if (!this.showForm) {
    this.bookToEdit = undefined; // Pulisce quando chiudi
  } else {
    // Se apro con il tasto "Aggiungi", assicuro che sia vuoto
    this.bookToEdit = undefined; 
  }
}

  ngOnInit(): void {
    this.loadBooks();
  }

  get filteredBooks() {
  return this.books.filter(book => 
    book.title.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
    book.genre.toLowerCase().includes(this.searchTerm.toLowerCase())
  );
}

  loadBooks() {
  this.bookService.getBooks().subscribe(data => {
    this.books = data;
    this.bookToEdit = undefined; // Reset modifica
    this.cd.detectChanges();
  });
}

  // Aggiungi questo metodo dentro la classe BookListComponent
deleteBook(id: number): void {
  if (confirm('Are you sure you want to delete this book?')) {
    this.bookService.deleteBook(id).subscribe({
      next: () => {
        // Filtriamo la lista locale per rimuovere il libro senza ricaricare la pagina
        this.books = this.books.filter(b => b.id !== id);
        console.log('Book successfully deleted');
        this.cd.detectChanges();
      },
      error: (err) => {
        console.error('Error during book delete operation', err);
        alert('Error during book delete operation');
      }
    });
  }
}

// Variable to keep track of the editing book
bookToEdit?: Book;

editBook(book: Book) {
  // Copy of the book to avoid concurrency issues
  this.bookToEdit = { ...book };
  this.showForm = true; 
  this.cd.detectChanges();
}
}