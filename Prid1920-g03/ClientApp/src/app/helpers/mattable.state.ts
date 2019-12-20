import { MatTableDataSource, MatSort, MatSortHeader, MatPaginator, PageEvent } from "@angular/material";

export class MatTableState {
  
    public pageIndex: number = 0;
    public pageSize: number;
    public filter: string = '';

    constructor( pageSize: number) {
      
        this.pageSize = pageSize;
    }

    public restoreState(dataSource: MatTableDataSource<any>) {
        this.setPage(dataSource.paginator, this.pageIndex, this.pageSize);
        this.setFilter(dataSource, this.filter);
    }

    public bind(dataSource: MatTableDataSource<any>) {
        
        dataSource.paginator.page.subscribe((e: PageEvent) => {
            this.pageIndex = e.pageIndex;
            this.pageSize = e.pageSize;
        });
    }

    

    // see: https://github.com/angular/components/issues/8417#issuecomment-453253715
    private setPage(paginator: MatPaginator, pageIndex: number, pageSize: number) {
        paginator.pageIndex = pageIndex;
        paginator._changePageSize(pageSize);
        //todo: utile ?
        //(this.paginator as any)._emitPageEvent(pageIndex);
    }

    private setFilter(dataSource: MatTableDataSource<any>, filter: string) {
        dataSource.filter = filter;
    }
}