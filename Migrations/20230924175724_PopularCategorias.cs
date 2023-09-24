﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanchesMac.Migrations
{
    /// <inheritdoc />
    public partial class PopularCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO categorias(Name, Description) " + 
                "VALUES('Normal', 'Lanches feito com ingredientes normais')");

            migrationBuilder.Sql("INSERT INTO categorias(Name, Description) " +
                "VALUES('Natural', 'Lanches feito com ingredientes integrais e naturais')");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");
        }
    }
}
