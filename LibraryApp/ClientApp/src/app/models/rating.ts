import { Book } from "./book";

export interface Rating {
    id: number;
    score: number;
    review: string;
    userId: number;
    assetId: number;
    ratedBook: Book;
}