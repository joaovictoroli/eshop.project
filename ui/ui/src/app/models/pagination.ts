export class Pagination {
  currentPage: number = 1;
  itemsPerPage?: number;
  totalItems?: number;
  totalPages?: number;
}

export class PaginatedResult<T> {
  result?: T;
  pagination?: Pagination;
}

export class QueryParams {
  name: string = '';
  minprice: number = 0;
  maxprice: number = 0;
  SubCategoryName: string = '';
  pageNumber: number = 1;
}
