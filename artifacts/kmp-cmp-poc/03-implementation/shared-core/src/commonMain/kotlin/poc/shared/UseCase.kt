package poc.shared

data class Item(val id: String, val title: String)

class ListItemsUseCase {
    fun execute(): List<Item> = listOf(
        Item("1", "Compose on Tizen feasibility"),
        Item("2", "Input and lifecycle validation"),
    )
}
