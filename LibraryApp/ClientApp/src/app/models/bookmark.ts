import { User } from "../shared/user.model";
import { Asset } from "./asset";

export interface Bookmark {
    id: number;
    user: User
    userId: number;
    asset: Asset;
    assetId: number;
}