import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { VehicleStockItem } from 'src/app/models/vehicleStockItem';
import { FormBuilder, FormGroup, FormControl, Validators, NgForm } from "@angular/forms";
import { Accessory } from '../../models/accessory';
import { EmitEvent, EventBusService, Events } from 'src/app/services/event-bus.service';
import { VehicleStockImage } from 'src/app/models/vehicleStockImage';
import * as cloneDeep from 'lodash/cloneDeep';

@Component({
  selector: 'app-stock-details',
  templateUrl: './stock-details.component.html',
  styleUrls: ['./stock-details.component.scss']
})

export class StockDetailsComponent implements OnInit, OnDestroy {
  @ViewChild('stockDetailsForm', { static: true }) stockDetailForm: NgForm;
  @ViewChild('UploadFileInput', { static: true }) uploadFileInput: ElementRef;


  constructor(private eventbus: EventBusService, private authorizeService: AuthorizeService) { }


  eventbusSubscription: Subscription;
  paramsSubscription: Subscription;

  stockItem: VehicleStockItem = new VehicleStockItem();
  // uploadForm: FormGroup;
  imageURL: string;
  myfilename = 'Select File/s (max 3)';

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

  //vehicleStockImages: VehicleStockImage[] = [new VehicleStockImage(), new VehicleStockImage(), new VehicleStockImage()];
  vehicleStockImages: VehicleStockImage[];


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

    if (this.stockItem && stock) {
      this.stockItem = cloneDeep(stock);
    }
    else {
      return;
    }

    this.vehicleStockImages = cloneDeep(stock.images);
    this.updateFileCountNameByLength(this.vehicleStockImages.length);
  }

  onSubmit() {
    this.stockItem.images = cloneDeep(this.vehicleStockImages);

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

      //Array.from(fileInput.target.files).forEach((file: File) => {
      //  console.log(file);
      //  //this.myfilename += file.name + ',';
      //});

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

            //if (this.cardImageBase64[0] == '') {
            //  this.cardImageBase64[0] = imgBase64Path;
            //} else if (this.cardImageBase64[1] == '') {
            //  this.cardImageBase64[1] = imgBase64Path;
            //} else if (this.cardImageBase64[2] == '') {
            //  this.cardImageBase64[2] = imgBase64Path;
            //}
          };
        };

        reader.readAsDataURL(fileInput.target.files[i]);
        i++;
      });
      //for (var i = 0; i < 3; i++) {
      //  const reader = new FileReader();
      //  reader.onload = (e: any) => {
      //    const image = new Image();
      //    image.src = e.target.result;
      //    image.onload = rs => {
      //      // Return Base64 Data URL
      //      const imgBase64Path = e.target.result;
      //      if (this.cardImageBase64[0] == '') {
      //        this.cardImageBase64[0] = imgBase64Path;
      //      } else if (this.cardImageBase64[1] == '') {
      //        this.cardImageBase64[1] = imgBase64Path;
      //      } else if (this.cardImageBase64[2] == '') {
      //        this.cardImageBase64[2] = imgBase64Path;
      //      }
      //    };
      //  };
      //  reader.readAsDataURL(fileInput.target.files[i]);
      //}

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

    //if (this.vehicleStockImages) {
    //  this.vehicleStockImages.forEach((element, index) => {
    //    if (element.image == src) delete this.vehicleStockImages[index];
    //  });
    //}

    //if (index > -1) {
    //  this.cardImageBase64[index]='';
    //}
    //const index = this.cardImageBase64.indexOf(src, 0);
    //if (index > -1) {
    //  this.cardImageBase64[index]='';
    //}
  }

}
