export interface Categoria {
  categoriaId: number;
  nombre: string;
  categoriaPadreId: number | null;
  categoriaPadreNombre: string | null;
}

export interface CreateCategoriaRequest {
  nombre: string;
  categoriaPadreId: number | null;
}
