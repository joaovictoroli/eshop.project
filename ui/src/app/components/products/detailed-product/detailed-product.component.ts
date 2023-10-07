import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Product } from 'src/app/models/product';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-detailed-product',
  templateUrl: './detailed-product.component.html',
  styleUrls: ['./detailed-product.component.scss'],
})
export class DetailedProductComponent {
  product: Product | undefined;

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private authService: AuthService,
    private toastr: ToastrService,
    private cartService: CartService
  ) {}

  ngOnInit() {
    const name = this.route.snapshot.paramMap.get('name');
    if (name) {
      this.productService.getProduct(name).subscribe({
        next: (product) => (this.product = product),
        error: (error) => console.error('Error fetching product:', error),
      });
    }
  }

  addToCart() {
    this.authService.currentUser$
      .subscribe((user) => {
        if (user) {
          this.cartService.addItem(this.product!);
        } else {  
          this.toastr.error(
            'Por favor, faça login para adicionar produtos ao carrinho.',
            'Erro'
          );
        }
      })
      .unsubscribe();
  }
}
