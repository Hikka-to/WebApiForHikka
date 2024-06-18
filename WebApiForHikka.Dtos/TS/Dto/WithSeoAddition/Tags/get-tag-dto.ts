/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { GetDtoWithSeoAddition } from "../../../Shared/get-dto-with-seo-addition";

export interface GetTagDto extends GetDtoWithSeoAddition {
    name: string;
    engName: string;
    alises: string[];
    isGenre: boolean;
    parentTagId: string;
}
