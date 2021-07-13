import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { VehicleStockItem } from 'src/app/models/vehicleStockItem';
import { FormBuilder, FormGroup, FormControl, Validators, NgForm } from "@angular/forms";
import { Accessory } from '../../models/accessory';
import { EmitEvent, EventBusService, Events } from 'src/app/services/event-bus.service';

@Component({
  selector: 'app-stock-details',
  templateUrl: './stock-details.component.html',
  styleUrls: ['./stock-details.component.css']
})

export class StockDetailsComponent implements OnInit, OnDestroy {
  @ViewChild('stockDetailsForm', { static: false }) stockDetailForm: NgForm;
  eventbusSubscription: Subscription;
  paramsSubscription: Subscription;

  stockItem: VehicleStockItem = new VehicleStockItem();
  // uploadForm: FormGroup;
  imageURL: string;

  entry: FormControl = new FormControl('', [Validators.required]);

  accessoriesList: Accessory[] = [
    { name: 'Electric windows', description: '' },
    { name: 'Power steering', description: '' },
    { name: 'Central locking', description: '' },
    { name: 'Cruise control', description: '' },
    { name: 'Air conditioning', description: '' },
    { name: 'Climate control', description: '' },
    { name: 'CD Player', description: '' },
    { name: 'Bluetooth connectivity', description: '' },
    { name: 'USB Port', description: '' },
    { name: 'Auxiliary Input', description: '' },
    { name: 'Navigation', description: '' },
    { name: 'Sunroof', description: '' }];


  constructor(private eventbus: EventBusService, private authorizeService: AuthorizeService) {
    // , public fb: FormBuilder
    // this.uploadForm = this.fb.group({
    //   avatar: [null],
    //   name: ['']
    // });
  }

  ngOnInit() {
    //Use event bus to provide loosely coupled communication
    this.eventbusSubscription = this.eventbus.on(Events.StockSelected, (stock => this.onStockSelectedEvent(stock)));
  }

  OnSubmitDetails() {
    console.log(this.stockItem);
  }

  ngOnDestroy() {
    if (this.paramsSubscription) {
      this.paramsSubscription.unsubscribe();
    }
    if (this.eventbusSubscription) {
      this.eventbusSubscription.unsubscribe();
    }
  }
  getErrorMessage() {
    return this.entry.hasError('required') ? 'You must enter a value' :
      '';

    //async onSaveDetails() {
    //  let userName = await this.authorizeService.getUser().pipe(map(u => u && u.name));
    //  // this.stockItem.createdBy = userName.tostring();
    //}
  }

  onStockSelectedEvent(stock: VehicleStockItem) {
    if (this.stockItem && stock) {
      this.stockItem = stock;
    }
  }

  onSubmit() {
    console.log(this.stockDetailForm);
    console.log(this.stockItem);
    this.eventbus.emit(new EmitEvent(Events.StockSubmitted, this.stockItem));
  }

  onCancel() {
    this.eventbus.emit(new EmitEvent(Events.StockCancelled, null));
  }


  // Submit Form
  // submit() {
  //   console.log(this.uploadForm.value);
  // }

  // Image Preview
  // showPreview(event) {
  //   const file = (event.target as HTMLInputElement).files[0];
  //   this.uploadForm.patchValue({
  //     avatar: file
  //   });
  //   this.uploadForm.get('avatar').updateValueAndValidity();

  //   // File Preview
  //   const reader = new FileReader();
  //   reader.onload = () => {
  //     this.imageURL = reader.result as string;
  //   }
  //   reader.readAsDataURL(file);
  // }
}
