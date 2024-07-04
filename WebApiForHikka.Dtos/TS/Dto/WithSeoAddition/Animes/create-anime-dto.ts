/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { CreateDtoWithSeoAddition } from "../../../Shared/create-dto-with-seo-addition";
import { IFormFile } from "./i-form-file";

export interface CreateAnimeDto extends CreateDtoWithSeoAddition {
    kindId: string;
    statusId: string;
    periodId: string;
    restrictedRatingId: string;
    sourceId: string;
    name: string;
    imageName: string;
    romajiName: string;
    nativeName: string;
    posterImage: IFormFile;
    posterColors: number[];
    avgDuration: number;
    howManyEpisodes: number;
    firstAirDate: Date;
    lastAirDate: Date;
    tmdbId: number;
    shikimoriId: number;
    shikimoriScore: number;
    tmdbScore: number;
    imdbScore: number;
    isPublished: boolean;
    publishedAt: Date;
    updatedAt: Date;
    createdAt: Date;
}
