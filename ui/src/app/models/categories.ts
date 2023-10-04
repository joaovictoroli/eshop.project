export interface Category {
  id: number;
  name: string;
  description: string;
  subCategories: SubCategory[] | undefined;
}

export interface SubCategory {
  name: string;
  description: string;
  categoryId: number;
}
