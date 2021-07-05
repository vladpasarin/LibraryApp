import { Asset } from "./asset";

export interface GenericBook {
    id: number;
    title: string;
    author: string;
    isbn: string;
    asin: string;
    publicationYear: number;
    edition: string;
    publisher: string;
    deweyIndex: string;
    language: string;
    summary: string;
    assetId: number;
    asset: Asset;
    size: number;
    lengthMinutes: number;
}