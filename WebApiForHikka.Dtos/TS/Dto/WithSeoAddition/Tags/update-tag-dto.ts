/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { UpdateDtoWithSeoAddition } from "../../../Shared/update-dto-with-seo-addition";

export interface UpdateTagDto extends UpdateDtoWithSeoAddition {
    name: string;
    engName: string;
    alises: string[];
    isGenre: boolean;
    parentTagId: string;
}
