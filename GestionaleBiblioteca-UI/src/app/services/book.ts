import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Book, Author } from '../models/library.model';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  
  private apiUrlBooks = 'https://localhost:7001/api/books'; 

  private apiUrlAuthors = 'https://localhost:7001/api/authors';

  constructor(private http: HttpClient) { }

  //Retrieves all books
  getBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(this.apiUrlBooks);
  }

  // Retrieves single book by ID
  getBookById(id: number): Observable<Book> {
    return this.http.get<Book>(`${this.apiUrlBooks}/${id}`);
  }

  // Adds a book
  addBook(book: Book): Observable<Book> {
    return this.http.post<Book>(this.apiUrlBooks, book);
  }

  // Deletes a book
  deleteBook(id: number): Observable<void> {
  return this.http.delete<void>(`${this.apiUrlBooks}/${id}`);
}

// Adds an author
addAuthor(author: Author): Observable<Author> {
  return this.http.post<Author>(this.apiUrlAuthors, author);
}

// Deletes an author
deleteAuthor(id : number): Observable<void> {
  return this.http.delete<void>(`${this.apiUrlAuthors}/${id}`);
}

// Retrieves all authors
getAuthors(): Observable<Author[]> {
  return this.http.get<Author[]>(this.apiUrlAuthors);
}

// Updates a Book information
updateBook(id: number, book: Book): Observable<void> {
  return this.http.put<void>(`${this.apiUrlBooks}/${id}`, book);
}

// Updates an Author information
updateAuthor(id: number, author: Author): Observable<void> {
  return this.http.put<void>(`${this.apiUrlAuthors}/${id}`, author);
}
}