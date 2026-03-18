// ========================================
// Theme Toggle
// ========================================
document.addEventListener('DOMContentLoaded', function () {
    const themeToggle = document.getElementById('theme-toggle');
    const themeIcon = document.getElementById('theme-icon');
    const html = document.documentElement;

    // Check saved theme
    const savedTheme = localStorage.getItem('theme') || 'light';
    if (savedTheme === 'dark') {
        html.setAttribute('data-theme', 'dark');
        themeIcon.classList.replace('bi-moon-fill', 'bi-sun-fill');
    }

    if (themeToggle) {
        themeToggle.addEventListener('click', function () {
            const currentTheme = html.getAttribute('data-theme');
            const newTheme = currentTheme === 'dark' ? 'light' : 'dark';
            
            if (newTheme === 'dark') {
                html.setAttribute('data-theme', 'dark');
                themeIcon.classList.replace('bi-moon-fill', 'bi-sun-fill');
            } else {
                html.removeAttribute('data-theme');
                themeIcon.classList.replace('bi-sun-fill', 'bi-moon-fill');
            }
            
            localStorage.setItem('theme', newTheme);
        });
    }
});

// ========================================
// Smooth Scroll for Navigation
// ========================================
document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        e.preventDefault();
        const target = document.querySelector(this.getAttribute('href'));
        if (target) {
            const navbarHeight = document.querySelector('#main-navbar').offsetHeight;
            const targetPosition = target.offsetTop - navbarHeight - 20;
            
            window.scrollTo({
                top: targetPosition,
                behavior: 'smooth'
            });
            
            // Close mobile menu if open
            const navbarCollapse = document.querySelector('.navbar-collapse');
            if (navbarCollapse && navbarCollapse.classList.contains('show')) {
                navbarCollapse.classList.remove('show');
            }
        }
    });
});

// ========================================
// Navbar Background on Scroll
// ========================================
window.addEventListener('scroll', function () {
    const navbar = document.querySelector('#main-navbar');
    if (window.scrollY > 50) {
        navbar.style.boxShadow = '0 2px 20px rgba(0, 0, 0, 0.1)';
    } else {
        navbar.style.boxShadow = '0 0.125rem 0.25rem rgba(0, 0, 0, 0.075)';
    }
});

// ========================================
// Scroll Reveal Animation
// ========================================
const observerOptions = {
    root: null,
    rootMargin: '0px',
    threshold: 0.1
};

const observer = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            entry.target.classList.add('revealed');
            observer.unobserve(entry.target);
        }
    });
}, observerOptions);

document.querySelectorAll('.skill-card, .about-card, .additional-skill-item').forEach(el => {
    el.style.opacity = '0';
    el.style.transform = 'translateY(30px)';
    el.style.transition = 'opacity 0.6s ease, transform 0.6s ease';
    observer.observe(el);
});

// Add revealed class styles
const style = document.createElement('style');
style.textContent = `
    .revealed {
        opacity: 1 !important;
        transform: translateY(0) !important;
    }
    
    .skill-card:nth-child(1) { transition-delay: 0.1s; }
    .skill-card:nth-child(2) { transition-delay: 0.2s; }
    .additional-skill-item:nth-child(1) { transition-delay: 0.1s; }
    .additional-skill-item:nth-child(2) { transition-delay: 0.15s; }
    .additional-skill-item:nth-child(3) { transition-delay: 0.2s; }
    .additional-skill-item:nth-child(4) { transition-delay: 0.25s; }
    .additional-skill-item:nth-child(5) { transition-delay: 0.3s; }
    .additional-skill-item:nth-child(6) { transition-delay: 0.35s; }
`;
document.head.appendChild(style);

// ========================================
// Active Nav Link on Scroll
// ========================================
const sections = document.querySelectorAll('section[id]');
const navLinks = document.querySelectorAll('.nav-link');

window.addEventListener('scroll', () => {
    let current = '';
    
    sections.forEach(section => {
        const sectionTop = section.offsetTop;
        const sectionHeight = section.clientHeight;
        if (scrollY >= sectionTop - 200) {
            current = section.getAttribute('id');
        }
    });
    
    navLinks.forEach(link => {
        link.classList.remove('active');
        if (link.getAttribute('href') === '#' + current) {
            link.classList.add('active');
        }
    });
});

// ========================================
// Mobile Navigation (Hamburger Menu)
// ========================================
function initMobileNav() {
    const hamburger = document.querySelector('.hamburger-menu');
    const mobilePanel = document.querySelector('.mobile-nav-panel');
    const overlay = document.querySelector('.mobile-nav-overlay');
    
    if (!hamburger || !mobilePanel) return;
    
    hamburger.addEventListener('click', () => {
        const isOpen = mobilePanel.classList.contains('open');
        const icon = hamburger.querySelector('i');
        
        if (isOpen) {
            mobilePanel.classList.remove('open');
            overlay.classList.remove('open');
            hamburger.setAttribute('aria-expanded', 'false');
            icon.classList.remove('bi-x');
            icon.classList.add('bi-list');
        } else {
            mobilePanel.classList.add('open');
            overlay.classList.add('open');
            hamburger.setAttribute('aria-expanded', 'true');
            icon.classList.remove('bi-list');
            icon.classList.add('bi-x');
        }
    });
    
    // Close on overlay click
    overlay.addEventListener('click', () => {
        mobilePanel.classList.remove('open');
        overlay.classList.remove('open');
        hamburger.setAttribute('aria-expanded', 'false');
        const icon = hamburger.querySelector('i');
        icon.classList.remove('bi-x');
        icon.classList.add('bi-list');
    });
    
    // Close on nav link click
    mobilePanel.querySelectorAll('.nav-link').forEach(link => {
        link.addEventListener('click', () => {
            mobilePanel.classList.remove('open');
            overlay.classList.remove('open');
            hamburger.setAttribute('aria-expanded', 'false');
            const icon = hamburger.querySelector('i');
            icon.classList.remove('bi-x');
            icon.classList.add('bi-list');
        });
    });
    
    // Mobile theme toggle
    const mobileThemeToggle = document.getElementById('theme-toggle-mobile');
    const mobileThemeIcon = document.getElementById('theme-icon-mobile');
    
    if (mobileThemeToggle && mobileThemeIcon) {
        mobileThemeToggle.addEventListener('click', function() {
            const currentTheme = document.documentElement.getAttribute('data-theme');
            const newTheme = currentTheme === 'dark' ? 'light' : 'dark';
            
            if (newTheme === 'dark') {
                document.documentElement.setAttribute('data-theme', 'dark');
                mobileThemeIcon.classList.replace('bi-moon-fill', 'bi-sun-fill');
            } else {
                document.documentElement.removeAttribute('data-theme');
                mobileThemeIcon.classList.replace('bi-sun-fill', 'bi-moon-fill');
            }
            
            localStorage.setItem('theme', newTheme);
            
            // Sync desktop toggle
            const desktopThemeIcon = document.getElementById('theme-icon');
            const desktopThemeToggle = document.getElementById('theme-toggle');
            if (desktopThemeIcon) {
                if (newTheme === 'dark') {
                    desktopThemeIcon.classList.replace('bi-moon-fill', 'bi-sun-fill');
                } else {
                    desktopThemeIcon.classList.replace('bi-sun-fill', 'bi-moon-fill');
                }
            }
        });
        
        // Sync initial state
        const savedTheme = localStorage.getItem('theme') || 'light';
        if (savedTheme === 'dark') {
            mobileThemeIcon.classList.replace('bi-moon-fill', 'bi-sun-fill');
        }
    }
}

// Initialize mobile navigation
document.addEventListener('DOMContentLoaded', function() {
    initMobileNav();
});

// ========================================
// Project Gallery Filtering
// ========================================
function initProjectFilter() {
    const filterButtons = document.querySelectorAll('.btn-filter');
    const projectItems = document.querySelectorAll('.project-item');
    
    if (filterButtons.length === 0 || projectItems.length === 0) return;
    
    filterButtons.forEach(button => {
        button.addEventListener('click', () => {
            const filter = button.getAttribute('data-filter');
            
            // Update active button
            filterButtons.forEach(btn => btn.classList.remove('active'));
            button.classList.add('active');
            
            // Filter projects
            projectItems.forEach(item => {
                const category = item.getAttribute('data-category');
                
                if (filter === 'all' || category === filter) {
                    item.classList.remove('hidden');
                    item.style.opacity = '0';
                    setTimeout(() => {
                        item.style.opacity = '1';
                    }, 50);
                } else {
                    item.classList.add('hidden');
                }
            });
        });
    });
}

// Initialize project filter
document.addEventListener('DOMContentLoaded', function() {
    initProjectFilter();
});

// ========================================
// Contact Form with Formspree
// ========================================
function initContactForm() {
    const form = document.getElementById('contact-form');
    const submitBtn = document.getElementById('submit-btn');
    const successAlert = document.getElementById('form-success');
    const errorAlert = document.getElementById('form-error');
    const spinner = submitBtn.querySelector('.spinner-border');
    const btnText = submitBtn.querySelector('.btn-text');
    
    if (!form) return;
    
    form.addEventListener('submit', async (e) => {
        e.preventDefault();
        
        // Show loading state
        submitBtn.disabled = true;
        spinner.classList.remove('d-none');
        btnText.textContent = 'Enviando...';
        successAlert.classList.add('d-none');
        errorAlert.classList.add('d-none');
        
        const formData = new FormData(form);
        const data = {
            name: formData.get('name'),
            email: formData.get('email'),
            message: formData.get('message')
        };
        
        try {
            const response = await fetch('https://formspree.io/f/xlgpoyqz', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },
                body: JSON.stringify(data)
            });
            
            if (response.ok) {
                successAlert.textContent = '¡Mensaje enviado exitosamente! Te responderé pronto.';
                successAlert.classList.remove('d-none');
                form.reset();
            } else {
                throw new Error('Formspree error');
            }
        } catch (error) {
            errorAlert.textContent = 'Error al enviar el mensaje. Por favor intenta de nuevo.';
            errorAlert.classList.remove('d-none');
        } finally {
            submitBtn.disabled = false;
            spinner.classList.add('d-none');
            btnText.textContent = 'Enviar Mensaje';
        }
    });
}

// Initialize contact form
document.addEventListener('DOMContentLoaded', function() {
    initContactForm();
});
