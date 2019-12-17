# Windows-gallery

Small application that reads all image files inside a selected folder (set from menu File-> Load directory) and shows them in a simple UI like a gallery.

When you click on an image, a new window with the selected image is opened. In this detail window at the bottom center, there is a ComboBox with some image processor that can be applied to the current image, the current image processors are just to ilustrate the architecture flexibility in provide a easy way to implement new image processor you want.

The user can select a image processor and click on the button Apply, then the processor do it job.

# Application architecture

The application is a WPF app based on MVVM pattern and uses Caliburn framework. In a high-level view, we have some components that compose this software:

 - Views: Contains the UI definition files based on XAML language. Make use of ViewModels.
 - ViewModels: Contains the ViewModel classes, that take care of store the views state, bring the view properties to be binded to the views and connect UI with servers and other background providers. Make use of services and models.
 - Models: Model classes that represents the application domain or messages between objects. Used by many components.
 - Controls: Custon UI components.
 - Services: This component is a representation for a set of C# interfaces that defines the behavior of Services provided by the application. All services must be defined as interfaces to decouple dependencies between classes.
  - Providers: This component contains the classes implementation of Services interfaces.
  
 The high level architecture is decribed below:
  
![Components](docs-images/Components.PNG?raw=true "Components Diagram")

# Proposed architecture for include new image processor features

The current application architecture is open for extension of image processing features.
This was achieved by defining a common interface that must be implemented by all intended image processor services. Some other architectural aspects were developed to make possible a fast feature extension:

 - Every image processor service must implement the interface IImageProcessor;
 - The ViewModels that want make use of image processors must request IoC.GetAllInstances for type IImageProcessor and store all references inside a List of ImageProcessors.
 - As every image processor implements the same interface IImageProcessor, it's easy deal with them workflow: Get the image processor, set processor image source, request ProcessImage and wait for it result (BitmapImage). Optionally, the caller can request a list of objects called Metadata, that is a generic argument store which can be cast to same future Model.
 
 The class structure for this architecture is shown below:
 
 ![Classes](docs-images/Classes.PNG?raw=true "Classes Diagram")

 
 The full workflow can be summarized by:
 
 - Each IImageProcessor implementation must be registered in the IoC container during bootstrap (class AppBootstrap.cs method Configure);
 - Currently, only ImageDetailsViewModel makes use of imaging processor features, so, it contains a property of type List<IImageProcessor>. This list is filled by calling IoC.GetAllInstances for type IImageProcessor (previously registered);
 - On ImageDetailsView, the user can select any ImageProcessor and apply it to the image.
 - As any ImageProcessor implements the same interface, we must call SetImageSource and ProcessImage. The result of this calls is a BitmapImage that can be rendered in a WPF window.
 - If necessary, the caller can get the image processor Metadata property, that is a generic storage for anything the processor want to expose after it job. The metadata is a list of object (as generic as possible) that can be cast to some implemented model in the future.
 
 The sequence diagram below illustrates the current dynamic in the app:
 
 ![Sequence](docs-images/Sequence.png?raw=true "Sequence Diagram")
 
