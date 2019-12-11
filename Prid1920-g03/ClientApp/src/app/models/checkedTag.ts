
export class CheckedTag
{
    name: string;
    isChecked: boolean;

    constructor(data: any) {
        if(data) {
          this.isChecked = false;
          this.name = data.name;
        }
      }
}