import { Component, EventEmitter, OnInit ,Input, OnChanges, Output, ChangeDetectorRef} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; // Fondamentale per i form
import { BookService } from '../../services/book';
import { Author } from '../../models/library.model';
import { Book } from '../../models/library.model';

@Component({
  selector: 'app-book-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './book-form.html',
  styleUrl: './book-form.css'
})
export class BookFormComponent implements OnInit , OnChanges{
  authors: Author[] = [];
  @Output() bookAdded = new EventEmitter<void>();
@Input() editData?: Book; // Riceve i dati dal padre

  ngOnChanges(): void {
  if (this.editData) {
    // Modalità MODIFICA: carico i dati esistenti
    this.newBook = { ...this.editData };
  } else {
    // Modalità AGGIUNTA: resetto ai valori iniziali
    this.newBook = { 
      id: 0, 
      title: '', 
      genre: '',
      authorId: 0, 
      publishingYear: new Date().getFullYear() 
    };
  }
  this.cd.detectChanges();
}
  
  // Oggetto che conterrà i dati del nuovo libro
  newBook = {
    id: 0, // ID initalized in Angular
    title: '',
    genre: '',
    publishingYear: new Date().getFullYear(),
    authorId: 0
  };

  showAuthorFields = false;

newAuthor: Author = { id: 0, name: '', surname: '' };

  constructor(private bookService: BookService,
              private cd: ChangeDetectorRef) 
    {}

  ngOnInit(): void {
    this.loadAuthors();
  }

  loadAuthors() {
    this.bookService.getAuthors().subscribe({
      next: (data) => {
        this.authors = data;
        this.cd.detectChanges(); // Forcing Combo box update
      },
      error: (err) => console.error('Errore caricamento autori:', err)
    });
  }

  saveBook() {
    if (this.newBook.id > 0) {
      // MODALITÀ UPDATE
      this.bookService.updateBook(this.newBook.id, this.newBook).subscribe({
        next: () => this.bookAdded.emit()
      });
    } else {
      // MODALITÀ ADD (quella di prima)
      this.bookService.addBook(this.newBook).subscribe({
        next: () => this.bookAdded.emit()
      });
    }
  }

  // Metodo per salvare l'autore e selezionarlo automaticamente
saveAuthor() {
  this.bookService.addAuthor(this.newAuthor).subscribe({
    next: (authorCreato) => {
      alert('Autore creato con successo!');
      this.authors.push(authorCreato); // Lo aggiungiamo alla lista locale
      this.newBook.authorId = authorCreato.id; // Lo selezioniamo nel form libro
      this.showAuthorFields = false; // Chiudiamo il mini-form autore
      this.newAuthor = { id: 0, name: '', surname: '' }; // Resettiamo
      this.cd.detectChanges(); // Forcing update
    },
    error: (err) => console.error('Errore creazione autore', err)
  });
}
}