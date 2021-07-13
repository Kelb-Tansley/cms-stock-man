import { Component, ElementRef, Inject, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { Accessory, VehicleStockItem } from 'src/app/models/vehicleStockItem';
import { FormControl, Validators, NgForm } from "@angular/forms";
import { EmitEvent, EventBusService, Events } from 'src/app/services/event-bus.service';
import { VehicleStockImage } from 'src/app/models/vehicleStockImage';
import * as cloneDeep from 'lodash/cloneDeep';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-stock-details',
  templateUrl: './stock-details.component.html',
  styleUrls: ['./stock-details.component.scss']
})

export class StockDetailsComponent implements OnInit, OnDestroy {
  @ViewChild('stockDetailsForm', { static: true }) stockDetailForm: NgForm;
  @ViewChild('UploadFileInput', { static: true }) uploadFileInput: ElementRef;

  accessoriesForm = new FormControl();

  http: HttpClient;
  baseUrl: string;

  constructor(private eventbus: EventBusService, http: HttpClient, @Inject('BASE_URL') baseUrl: string, private authorizeService: AuthorizeService) {
    this.http = http;
    this.baseUrl = baseUrl
  }

  eventbusSubscription: Subscription;
  paramsSubscription: Subscription;
  accesoriesSubscription: Subscription;

  stockItem: VehicleStockItem = new VehicleStockItem();

  imageURL: string;
  myfilename = 'Select File/s (max 3)';

  entry: FormControl = new FormControl('', [Validators.required]);

  accessoriesList: Accessory[];

  defaultImage = new Image();
  imageError: string;
  isImageSaved: boolean;

  vehicleStockImages: VehicleStockImage[];

  async ngOnInit() {
    //Use event bus to provide loosely coupled communication
    this.eventbusSubscription = this.eventbus.on(Events.StockSelected, (stock => this.onStockSelectedEvent(stock)));

    let endPoint = this.baseUrl + 'accessories';

    this.accesoriesSubscription = await this.http.get<Accessory[]>(endPoint).subscribe(result => {
      this.accessoriesList = result;
    }, error => console.error(error));
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
    if (this.accesoriesSubscription) {
      this.accesoriesSubscription.unsubscribe();
    }
  }

  getErrorMessage() {
    return this.entry.hasError('required') ? 'You must enter a value' :
      '';
  }

  onStockSelectedEvent(stock: VehicleStockItem) {
    if (this.stockItem && stock) {
      this.stockItem = cloneDeep(stock);
    }
    else {
      return;
    }

    this.accessoriesForm.setValue(stock.accessories);
    this.vehicleStockImages = cloneDeep(stock.images);
    this.updateFileCountNameByLength(this.vehicleStockImages.length);
  }

  onSubmit() {
    this.stockItem.images = cloneDeep(this.vehicleStockImages);
    this.stockItem.accessories = this.accessoriesForm.value;

    console.log(this.stockItem);
    this.eventbus.emit(new EmitEvent(Events.StockSubmitted, this.stockItem));
  }

  onCancel() {
    this.eventbus.emit(new EmitEvent(Events.StockCancelled, null));
  }

  fileChangeEvent(fileInput: any) {
    if (fileInput.target.files && fileInput.target.files[0]) {
      if (fileInput.target.files.length > 3) {
        alert("Only 3 images accepted.");
        fileInput.target.preventDefault();
        return;
      }


      //Update the selction count visual
      this.updateFileCountNameByLength(fileInput.target.files.length);

      this.vehicleStockImages = [];

      let i = 0;
      Array.from(fileInput.target.files).forEach((file: File) => {

        const reader = new FileReader();
        reader.onload = (e: any) => {
          const image = new Image();
          image.src = e.target.result;
          image.onload = rs => {

            // Return Base64 Data URL
            const imgBase64Path = e.target.result;

            if (!this.vehicleStockImages || this.vehicleStockImages.length == 0) {
              this.vehicleStockImages = [{ isPrimary: true, stockImage: imgBase64Path, name: '', id: 0 }];
            } else {
              this.vehicleStockImages.push({ isPrimary: false, stockImage: imgBase64Path, name: '', id: 0 });
            }
          };
        };

        reader.readAsDataURL(fileInput.target.files[i]);
        i++;
      });

      // Reset File Input to Select Same file again
      this.uploadFileInput.nativeElement.value = "";
    }
    else {
      this.myfilename = 'Select File/s (max 3)';
    }
  }

  updateFileCountNameByLength(count: number) {
    this.myfilename = '';
    if (count > 1) {
      this.myfilename = '(' + count + ') Images Entered';
    } else if (count == 1) {
      this.myfilename = '(' + count + ') Image Entered';
    } else if (count == 0) {
      this.myfilename = 'Select File/s (max 3)';
    }
  }

  removeImage(src: VehicleStockImage) {
    const index = this.vehicleStockImages.indexOf(src, 0);
    if (index > -1) {
      this.vehicleStockImages.splice(index, 1);
    }

    this.updateFileCountNameByLength(this.vehicleStockImages.length);
  }
}
