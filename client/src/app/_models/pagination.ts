export interface pagination {
  currentPage: number;
  itemsPerPage: number;
  totalItems: number;
  totalPages: number;
}

export class PaginatedResults<T> {
  result?: T;
  pagination?: pagination; // Fish out the pagniation results before using this class
}
