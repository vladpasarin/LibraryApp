import { Book } from "./book";

export interface Quote {
    id: number;
    content: string;
    userId: number;
    bookId: number;
    book: Book;
}