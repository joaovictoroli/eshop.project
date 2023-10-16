import { AbstractControl, ValidatorFn } from '@angular/forms';

export class CustomValidators {
  static eightDigitsPattern(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const valid = /^\d{8}$/.test(control.value);
      return valid ? null : { notEightDigits: true };
    };
  }

  static numericPattern(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const valid = /^\d*$/.test(control.value);
      return valid ? null : { notNumeric: true };
    };
  }

  static noSpecialCharactersExceptSpace(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      if (control.value) {
        const valid = /^[a-zA-Z0-9 ]+$/.test(control.value);
        return valid ? null : { specialCharactersExceptSpaceFound: true };
      }
      return null;

      //except spacebar
    };
  }

  static noSpecialCharacters(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      if (control.value) {
        const valid = /^[a-zA-Z0-9 ]+$/.test(control.value);
        return valid ? null : { specialCharactersFound: true };
      }
      return null;
    };
  }

  static matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value === control.parent?.get(matchTo)?.value
        ? null
        : { notMatching: true };
    };
  }

  static decimalNumber(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      if (control.value) {
        const valid = /^[0-9]*\.?[0-9]+$/.test(control.value);
        return valid ? null : { invalidDecimal: true };
      }
      return null;
    };
  }
}
