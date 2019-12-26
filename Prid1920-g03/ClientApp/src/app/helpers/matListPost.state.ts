import { MatTableDataSource, MatPaginator, PageEvent } from "@angular/material";

export class MatListPostState {
    
    public pageIndex: number = 0;
    public pageSize: number;
    public filter: string = '';

    constructor(pageSize: number) {
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

    private setPage(paginator: MatPaginator, pageIndex: number, pageSize: number) {
        paginator.pageIndex = pageIndex;
        paginator._changePageSize(pageSize);
    }

    private setFilter(dataSource: MatTableDataSource<any>, filter: string) {
        dataSource.filter = filter;
    }
}