export interface Category {
  id: number;
  name: string;
  description: string;
  subCategories: SubCategory[] | undefined;
}

export interface SubCategory {
  id: number;
  name: string;
  description: string;
  categoryId: number;
}

export interface AddCategory {
  name: string;
  description: string;
}

export interface AddSubCategory {
  name: string;
  description: string;
  categoryId: number;
}
