export class Pagination {
  currentPage: number = 1;
  itemsPerPage?: number = 10;
  totalItems?: number = 14;
  totalPages?: number = 2;
}

export class PaginatedResult<T> {
  result?: T;
  pagination?: Pagination;
}

export class QueryParams {
  private _name: string = '';
  private _minprice: number = 0;
  private _maxprice: number = 0;
  private _SubCategoryName: string = '';
  private _pageNumber: number = 1;

  get name(): string {
    return this._name;
  }

  set name(value: string) {
    this._name = value ?? '';
  }

  get minprice(): number {
    return this._minprice;
  }

  set minprice(value: number) {
    this._minprice = value ?? 0;
  }

  get maxprice(): number {
    return this._maxprice;
  }

  set maxprice(value: number) {
    this._maxprice = value ?? 0;
  }

  get subCategoryName(): string {
    return this._SubCategoryName;
  }

  set subCategoryName(value: string) {
    this._SubCategoryName = value ?? '';
  }

  get pageNumber(): number {
    return this._pageNumber;
  }

  set pageNumber(value: number) {
    this._pageNumber = value ?? 1;
  }
}
