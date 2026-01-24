export interface Author {
  id: number;
  name: string;
  surname: string;
  // Book list not included to avoid loops
}

export interface Book {
  id: number;
  title: string;
  genre: string;
  authorId: number;
  author?: Author; // Same as .Include(l => l.Author)
  publishingYear: number;
}