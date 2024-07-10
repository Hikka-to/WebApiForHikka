/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ModelDto } from "../../Shared/model-dto";
import { SocialType } from "./social-type";

export interface UpdateSeoAdditionDto extends ModelDto {
    id: string;
    slug: string;
    title: string;
    description: string;
    image: string;
    imageAlt: string;
    socialTitle: string;
    socialType: SocialType;
    socialImage: string;
    socialImageAlt: string;
}
