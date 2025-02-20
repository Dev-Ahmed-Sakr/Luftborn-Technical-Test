import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';  // Import RouterModule
import { ProductService, Product } from '../../services/product.service';

@Component({
  selector: 'app-product-form',
  standalone: true,  // Standalone component
  imports: [CommonModule, ReactiveFormsModule, RouterModule],  // Import RouterModule here
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.scss']
})
export class ProductFormComponent implements OnInit {
  productForm: FormGroup;
  productId: number | null = null;
  isEditMode: boolean = false;

  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private router: Router,  // Inject Router here
    private route: ActivatedRoute
  ) {
    this.productForm = this.fb.group({
      name: ['', Validators.required],
      price: ['', [Validators.required, Validators.min(0)]]
    });
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.productId = +id;
        this.isEditMode = true;
        this.loadProduct(this.productId);
      }
    });
  }

  loadProduct(id: number) {
    this.productService.getProduct(id).subscribe(product => {
      this.productForm.patchValue({
        name: product.name,
        price: product.price
      });
    });
  }

  onSubmit() {
    if (this.productForm.invalid) {
      return;
    }

    const product: Product = {
      id: this.productId || 0,
      ...this.productForm.value
    };

    if (this.isEditMode) {
      this.productService.updateProduct(product).subscribe(() => {
        this.router.navigate(['/products']);  // Navigate on successful update
      });
    } else {
      this.productService.addProduct(product).subscribe(() => {
        this.router.navigate(['/products']);  // Navigate on successful creation
      });
    }
  }

  onCancel() {
    this.router.navigate(['/products']);  // Navigate back on cancel
  }
}
