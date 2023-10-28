export interface Product {
  name: string;
  description: string;
  price: number;
  technicalInfo: string;
  imageUrl: string;
  subCategoryId: number;
  subCategoryName: string;
}

interface CartItem {
  product: Product;
  quantity: number;
}

interface AddProductDto {
  name: string;
  descescription: string;
  price: number;
  technicalInfo: string;
  subCategoryId: number;
  image: File;
}
