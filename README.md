## Shop system for vrchat sdk3.0 worlds  



https://user-images.githubusercontent.com/13707992/209089002-a7996d8e-63fc-4bcf-82bd-5315d898baac.mp4


# Make sure UdonSharp compiler is enabled for your project!  

## How-To-Use  
>example scene with basic shop and items to buy/sell  

Add "ShopSystemWorldManager" script to your world manager gameobject

Drag in as many shop prefabs as you want in your world  
add the prefabs to the list in "ShopSystemWorldManager" script inspector

Give items you want to be able to sell the "SellableItem" Script  
Change the sell price to whatever you want in the "SellableItem" script  

Items you want players to be able to buy and sell  
1. create Empty GameObject under "ShopSpawner"  
2. give the empty GameObject the "VRC OBJECT POOL" script  
3. add the amount of items you want players to be able to buy as children of the empty GameObject and drag them into the "VRC OBJECT POOL" from the previous step
4. now drag the empty GameObject with the "VRC OBJECT POOL" script into the the "ShopManager"  
5. set the prices/images/names in the "ShopManager" script  



