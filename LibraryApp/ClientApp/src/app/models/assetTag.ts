import { TagPlaceholder } from "@angular/compiler/src/i18n/i18n_ast";
import { Asset } from "./asset";
import { Tag } from "./tag";

export interface AssetTag {
    id: string;
    tagId: string;
    tag: Tag
    assetId: string;
    asset: Asset;
}