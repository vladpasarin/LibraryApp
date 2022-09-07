import { Asset } from "./asset";
import { Tag } from "./tag";

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
    tags: Tag[];
}