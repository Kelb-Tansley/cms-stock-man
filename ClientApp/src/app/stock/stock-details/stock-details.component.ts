import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { VehicleStockItem } from 'src/app/models/vehicleStockItem';
import { FormBuilder, FormGroup, FormControl, Validators, NgForm } from "@angular/forms";
import { Accessory } from '../../models/accessory';
import { EmitEvent, EventBusService, Events } from 'src/app/services/event-bus.service';
import { VehicleStockImage } from 'src/app/models/vehicleStockImage';

@Component({
  selector: 'app-stock-details',
  templateUrl: './stock-details.component.html',
  styleUrls: ['./stock-details.component.scss']
})

export class StockDetailsComponent implements OnInit, OnDestroy {
  @ViewChild('stockDetailsForm', { static: true }) stockDetailForm: NgForm;
  @ViewChild('UploadFileInput', { static: true }) uploadFileInput: ElementRef;
  myfilename = 'Select File/s (max 3)';

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

  defaultImage = new Image();
  imageError: string;
  isImageSaved: boolean;

  cardImageBase64: string[] = ['', '', ''];
  // cardImageTwoBase64: string = "";
  // cardImageThreeBase64: string = "";

  constructor(private eventbus: EventBusService, private authorizeService: AuthorizeService) {


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
  }

  onStockSelectedEvent(stock: VehicleStockItem) {
    if (this.cardImageBase64) { }
    this.cardImageBase64
    if (stock.images) {
      let primaryNotStored = false;
      stock.images.forEach((img: VehicleStockImage) => {
        if (img && this.cardImageBase64) {
          if (img.isPrimary && primaryNotStored) {
            this.cardImageBase64[0] = img.image;
            primaryNotStored = true;
          }
          else {
            if (this.cardImageBase64[1] == '') {
              this.cardImageBase64[1] = img.image;
            } else if (this.cardImageBase64[2] == '') {
              this.cardImageBase64[2] = img.image;
            } else if (this.cardImageBase64[0] == '') {
              this.cardImageBase64[0] = img.image;
            }
          }
        }
      });
    }

    if (this.stockItem && stock) {
      this.stockItem = stock;
    }
  }

  onSubmit() {


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

      this.myfilename = '';
      Array.from(fileInput.target.files).forEach((file: File) => {
        console.log(file);
        //this.myfilename += file.name + ',';
      });

      if (fileInput.target.files.length > 1) {
        this.myfilename = '(' + fileInput.target.files.length + ') Images Entered';
      } else if (fileInput.target.files.length == 1) {
        this.myfilename = '(' + fileInput.target.files.length + ') Image Entered';
      }


      for (var i = 0; i < 3; i++) {
        const reader = new FileReader();
        reader.onload = (e: any) => {
          const image = new Image();
          image.src = e.target.result;
          image.onload = rs => {
            // Return Base64 Data URL
            const imgBase64Path = e.target.result;
            if (this.cardImageBase64[0] == '') {
              this.cardImageBase64[0] = imgBase64Path;
            } else if (this.cardImageBase64[1] == '') {
              this.cardImageBase64[1] = imgBase64Path;
            } else if (this.cardImageBase64[2] == '') {
              this.cardImageBase64[2] = imgBase64Path;
            }
          };
        };
        reader.readAsDataURL(fileInput.target.files[i]);
      }

      // Reset File Input to Select Same file again
      this.uploadFileInput.nativeElement.value = "";
    }
    else {
      this.myfilename = 'Select File';
    }
  }
  removeImage(src: string) {
    const index = this.cardImageBase64.indexOf(src, 0);
    if (index > -1) {
      this.cardImageBase64[index]='';
    }
  }

}
