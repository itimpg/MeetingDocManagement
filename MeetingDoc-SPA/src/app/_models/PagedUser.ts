import { User } from './User';

export class PagedUser {
  currentPage: number;
  totalPage: number;
  totalRecord: number;
  data: User[];
}