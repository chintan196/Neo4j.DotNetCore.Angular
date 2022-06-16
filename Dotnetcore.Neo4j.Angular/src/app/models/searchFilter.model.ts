/**
 * Search filter
 */
 export class SearchFilter {
    title: string;
    released: number | null;
    producer: any;
    writer: any;
    director: any;
    personType: string;
    person: any;
  
    pageSize: number;
    currentPage: number;
  
    sortByField: string;
    sortOrder: string;
  
    /**
     * Creates an instance of search filter.
     * @param filter 
     */
    constructor(filter:SearchFilter)
    {    
      this.title = filter.title;
      this.released = filter.released;

      if (filter.personType == "Producer")
      {
        this.producer = filter.person;
        this.writer = null;
        this.director = null;
      }
      else if (filter.personType == "Writer")
      {
        this.producer = null
        this.writer = filter.person;
        this.director = null;        
      }
      else if (filter.personType == "Director")
      {
        this.producer = null
        this.writer = null;
        this.director = filter.person;
      }
      else if (filter.person.name != '' && filter.person.name != null)
      {
        this.producer = filter.person;
        this.writer = filter.person;
        this.director = filter.person;
      }

      this.pageSize = filter.pageSize;
      this.currentPage = filter.currentPage;
      this.sortByField = filter.sortByField;
      this.sortOrder = filter.sortOrder;
    }
  }