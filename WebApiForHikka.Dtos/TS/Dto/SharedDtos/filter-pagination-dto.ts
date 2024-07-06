/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { SortOrder } from "./sort-order";

export interface FilterPaginationDto {
    searchTerm: string;
    pageNumber: number;
    pageSize: number;
    column: string;
    sortOrder: SortOrder;
}
