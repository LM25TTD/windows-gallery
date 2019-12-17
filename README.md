# Windows-gallery

Small application that reads all image files inside a selected folder (set from menu File-> Load directory) and shows them in a simple UI like a gallery.

When you click on an image, a new window with the selected image is opened. In this detail window at the bottom center, there is a ComboBox with some image processor that can be applied to the current image, the current image processors are just to ilustrate the architecture flexibility in provide a easy way to implement new image processor you want.

The user can select a image processor and click on the button Apply, then the processor do it job.

# Application architecture

The application is based on MVVM pattern. In a high-level view, we have some components that compose this software:

 - Views: Contains the UI definition files based on XAML language. Make use of ViewModels.
 - ViewModels: Contains the ViewModel classes, that take care of store the views state, bring the view properties to be binded to the views and connect UI with servers and other background providers. Make use of services and models.
 - Models: Model classes that represents the application domain or messages between objects. Used by many components.
 - Controls: Custon UI components.
 - Services: This component is a representation for a set of C# interfaces that defines the behavior of Services provided by the application. All services must be defined as interfaces to decouple dependencies between classes.
  - Providers: This component contains the classes implementation of Services interfaces.
  
![Components](docs-iamges/Components.png?raw=true "Components Diagram")