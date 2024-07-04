/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { IHeaderDictionary } from "./i-header-dictionary";

export interface IFormFile {
    contentType: string;
    contentDisposition: string;
    headers: { [key: string]: string[]; };
    length: number;
    name: string;
    fileName: string;
}
